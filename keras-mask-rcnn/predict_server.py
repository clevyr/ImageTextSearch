# Copyright 2015 gRPC authors.
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#     http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
"""The Python implementation of the GRPC helloworld.Greeter server."""

from concurrent import futures
import logging
import grpc
import time

import image_tagger_pb2
import image_tagger_pb2_grpc

from mrcnn.config import Config
from mrcnn import model as modellib
from mrcnn import visualize
import numpy as np
import colorsys
import argparse
import imutils
import random
import cv2
import os
from keras.backend import clear_session, tf
os.environ['KMP_DUPLICATE_LIB_OK']='True'

_ONE_DAY_IN_SECONDS = 60 * 60 * 24

class_labels = 'coco_labels.txt'
weights_path = 'mask_rcnn_coco.h5'
CLASS_NAMES = open(class_labels).read().strip().split("\n")
hsv = [(i / len(CLASS_NAMES), 1, 1.0) for i in range(len(CLASS_NAMES))]
COLORS = list(map(lambda c: colorsys.hsv_to_rgb(*c), hsv))
random.seed(42)
random.shuffle(COLORS)

class SimpleConfig(Config):
	# give the configuration a recognizable name
	NAME = "coco_inference"

	# set the number of GPUs to use along with the number of images
	# per GPU
	GPU_COUNT = 1
	IMAGES_PER_GPU = 1

	# number of classes (we would normally add +1 for the background
	# but the background class is *already* included in the class
	# names)
	NUM_CLASSES = len(CLASS_NAMES)

############################################################################################
#load the model model
model = modellib.MaskRCNN(mode="inference", config=SimpleConfig(), model_dir=os.getcwd())
#load the weights
model.load_weights(weights_path, by_name=True)
# this is key : save the graph after loading the model
graph = tf.get_default_graph()
############################################################################################

class ImageTagger(image_tagger_pb2_grpc.ImageTaggerServicer):
    
    def __init__(self):
        # load the class label names from disk, one label per line
        
        self.test = 1
        # generate random (but visually distinct) colors for each class label
        # (thanks to Matterport Mask R-CNN for the method!)
    @staticmethod
    def prepare_image(filepath):
	
		# load the input image, convert it from BGR to RGB channel
		# ordering, and resize the image
        # nparr = np.frombuffer(image_bytes, np.uint8)
        # image = cv2.imdecode(nparr, 1)
        print('opening file', filepath)
        image = cv2.imread(filepath)
        image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
        image = imutils.resize(image, width=512)
        return image

    def TagImage(self, request, context):

        # loop over the predicted scores and class labels
        image = ImageTagger.prepare_image(request.filepath)
        (height, width, channels) = image.shape
        with graph.as_default():
            #Show AI model the image
            r = model.detect([image], verbose=0)[0]
            response = image_tagger_pb2.TagImageReply()
            for i in range(0, len(r["scores"])):
                item = response.items.add()
                # extract the bounding box information, class ID, label, predicted
                # probability, and visualization color
                (startY, startX, endY, endX) = r["rois"][i]

                classID = r["class_ids"][i]
                item.boundingBox.minX = startX/width
                item.boundingBox.minY = startY/height
                item.boundingBox.maxX = endX/width
                item.boundingBox.maxY = endY/height
                item.tag = CLASS_NAMES[classID]
                item.score = r["scores"][i]
            return response

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=1))
    image_tagger_pb2_grpc.add_ImageTaggerServicer_to_server(ImageTagger(), server)
    server.add_insecure_port('localhost:5001')
    server.start()
    #server.wait_for_termination()
    try:
        while True:
            time.sleep(_ONE_DAY_IN_SECONDS)
    except KeyboardInterrupt:
        server.stop(0)


if __name__ == '__main__':
    logging.basicConfig()
    serve()
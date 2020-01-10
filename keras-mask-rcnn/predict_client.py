from __future__ import print_function

import grpc

import image_tagger_pb2_grpc
import image_tagger_pb2


def run():
  channel = grpc.insecure_channel('localhost:5001')
  stub = image_tagger_pb2_grpc.ImageTaggerStub(channel)
  response = stub.TagImage(image_tagger_pb2.TagImageRequest())
  print("Greeter client received: " + repr(response))


if __name__ == '__main__':
  run()
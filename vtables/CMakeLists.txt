cmake_minimum_required(VERSION 3.9)
SET( CMAKE_LIBRARY_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/build")
SET( CMAKE_LIBRARY_OUTPUT_DIRECTORY_DEBUG "${CMAKE_SOURCE_DIR}/build")
SET( CMAKE_LIBRARY_OUTPUT_DIRECTORY_RELEASE "${CMAKE_SOURCE_DIR}/build")

SET( CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CMAKE_SOURCE_DIR}/build")
SET( CMAKE_RUNTIME_OUTPUT_DIRECTORY_DEBUG "${CMAKE_SOURCE_DIR}/build")
SET( CMAKE_RUNTIME_OUTPUT_DIRECTORY_RELEASE "${CMAKE_SOURCE_DIR}/build")
SET(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wno-attributes  -Wl,--no-undefined")
project(demo)

set(CMAKE_CXX_STANDARD 17)

add_library(demos SHARED simple_object.cpp weird_object.cpp complete_object.cpp ids.cpp callback_object.cpp ids.cpp )
add_executable(vtables main.c simple_object.cpp)

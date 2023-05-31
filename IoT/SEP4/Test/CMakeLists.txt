cmake_minimum_required(VERSION 3.19)
set(projectName VibHealthTest)
project(${projectName})

# GoogleTest requires at least C++14
set(CMAKE_CXX_STANDARD 14)

include(FetchContent)
FetchContent_Declare(
  googletest
  URL https://github.com/google/googletest/archive/03597a01ee50ed33e9dfd640b249b4be3799d395.zip
  DOWNLOAD_EXTRACT_TIMESTAMP true
)
# For Windows: Prevent overriding the parent project's compiler/linker settings
set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)

FetchContent_MakeAvailable(googletest )

set(SOURCES_TEST
    HumiTempTest.cpp
    SenderTest.cpp 
    ServoTest.cpp 
   

    FreeRTOS_defs/FreeRTOS_FFF_MocksDefinitions.cpp
    
    ../project_files/_c/HumiTempTask.c
    ../project_files/_c/CounterTask.c
    ../project_files/_c/ServoTask.c
    ../project_files/_c/SenderTask.c
    ../project_files/_c/ReceiverTask.c
    
)


# This makes the test.cpp files able to #include target libraries without errors.

# Because of the differencies in format used to pinpoint directories between local 
# testing and CD/CI testing the compiler evaluates in which environment it is located.
include_directories(
  .
  FreeRTOS_defs
  $<$<NOT:$<CONFIG:Local>>:${CMAKE_CURRENT_SOURCE_DIR}/../project_files/_h/>
  $<$<NOT:$<CONFIG:Local>>:${CMAKE_CURRENT_SOURCE_DIR}/../project_files/drivers/>
  $<$<NOT:$<CONFIG:Local>>:${CMAKE_CURRENT_SOURCE_DIR}/../project_files/_c/>
)


# Something to 
enable_testing()

add_executable(
  ${projectName}
  ${SOURCES_TEST}
)
target_link_libraries(
  ${projectName}
  GTest::gtest_main
)

include(GoogleTest)
gtest_discover_tests(${projectName})

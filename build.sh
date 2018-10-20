#!/bin/bash

DIR=$(readlink -f "$(dirname "$0")")
pushd ${DIR}

args="$@ /p:CustomBeforeMicrosoftCommonTargets=./empty.targets"
msbuild ${args}

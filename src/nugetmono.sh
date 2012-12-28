#!/bin/sh
BASEDIR=$(dirname $0)

mono --runtime=v4.0 "${BASEDIR}/../packages/NuGet.CommandLine.2.2.0/tools/NuGet.exe" $*

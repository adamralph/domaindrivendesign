#!/bin/sh
BASEDIR=$(dirname $0)

mono --runtime=v4.0 "${BASEDIR}/$NuGetConsole" $*

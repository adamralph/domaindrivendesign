#!/bin/sh
BASEDIR=$(dirname $0)

mono --runtime=v4.0 "${BASEDIR}/../packages/xunit.runners.1.9.1/tools/xunit.console.clr4.x86.exe" $*

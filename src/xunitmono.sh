#!/bin/sh
BASEDIR=$(dirname $0)

mono "${BASEDIR}/../packages/xunit.runners.1.9.1/tools/xunit.console.clr4.x86.exe" $*

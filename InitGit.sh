#!/bin/bash

echo "# GnuTerminalTetris" >> README.md
git init
git add README.md
git commit -m "update"
git remote add origin https://github.com/pompiduskus/GnuTerminalTetris.git
git push -u origin master

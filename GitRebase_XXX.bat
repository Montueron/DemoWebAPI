#!/bin/bash

git checkout master
git pull
git checkout Test_branch
git rebase master
echo %~n0
pause
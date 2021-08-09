@echo off

set fullFileName=%~n0
set branchNameFromFile=%fullFileName:*BranchName_=%

echo Full filename : %fullFileName%
echo Retrieved branch name : %branchNameFromFile%
set myCommand='git show-ref refs/heads/%branchNameFromFile%'
echo Command to be run : %myCommand%
for /f %%i in (%myCommand%) do set RESULT=%%i
IF "%RESULT%"=="" (
echo Branch not found
echo Program will exit
pause
exit
) ELSE (
echo Branch exists
git checkout master
git pull
git checkout %branchNameFromFile%
git merge master
echo Program will exit
pause
)
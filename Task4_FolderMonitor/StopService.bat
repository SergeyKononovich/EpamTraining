@ECHO OFF

echo Stop Win Service...
echo ---------------------------------------------------
NET STOP FolderMonitorService
echo ---------------------------------------------------
pause
echo Done.
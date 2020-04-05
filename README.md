# SaldoVFS
Virtual File System

I've tried to mostly show some concepts like DI, Exception Handling, Mocking in Unit Tests 

so there could be some displayed exception, which actually in production should be fixed:) or not shown 

There still some thing to improve, or refactor, like VirtualDir has some public vars, but i ve tried to somehow delegate responsibilities for different services 

like Backup, Display, AttachmentStorage + add configuration ability

Simplified virtual file system 

Commands Available:
/addFolder [name]  - Add Folder To Current
/removeFolder [name]  - Remove Folder From Current
/current  - Get Current Folder
/commands  - Get commands Menu
/cd [name] - switch to folder (..) go back
/addFile [name] [realFilePath]
/listFiles - List Files in Current Folder
/tree - List Files in Current Folder
/backup - Backup


Basically list of commands available 

initial folder is aka root, you can add ass many files or folder inside, you can step into folder or back to the parent with /cd .. command 

some commands are still not super properly validate 
so for example /addFolder test test could give not handled Exception, but if it's necessary i can fix this 

Tree is serialized into file, which name is in config, so when you open program it's deserialized back, even keeping Current reference. 

Real Files are stored in program Directory which is also in config file 

by generating Guid and adding them to the Virtual tree

{
  "databaseFile": "temp.dat",
  "directory": "files",
  "FtpAddress": "ftp://ftp.dlptest.com/",
  "FtpUser": "dlpuser@dlptest.com",
  "FtpPassword": "SzMf7rTE4pCrf9dV286GuNe4N"
}

Ftp is config for backup, not sure maybe it could be password is changing each day, but's it's openable in browser 



example 

EnterCommand

/tree
Displaying Virtual Tree

----- File a.ts3ad55f59-3b97-4cba-9c58-8987eeb7e5a3
----- Directory test
----- Directory boom


EnterCommand

/cd test
Switching Folder:

EnterCommand

/addFolder test2
Adding VirtualFolder.
Vritual Folder Added

EnterCommand

/cd test2
Switching Folder:

EnterCommand

/addFolder uuuu
Adding VirtualFolder.
Vritual Folder Added

EnterCommand

/tree
Displaying Virtual Tree

----- File a.ts3ad55f59-3b97-4cba-9c58-8987eeb7e5a3
----- Directory test
---------- Directory test2
--------------- Directory uuuu
----- Directory boom




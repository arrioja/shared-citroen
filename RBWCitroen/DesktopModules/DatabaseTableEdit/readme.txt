Database Table Edit - A table editor

Credits: 
Uwe Lesta
TripelASP for a nice table editor control

Another Rainbow desktop module - more to download on http://www.rainbowportal.net

INSTALL
1. Go to Admin all and to add module definition. 
2. Point to install.xml install file
3. Add the module to a page
4. Edit module settings: See below
5. Use it! ;o) 
Note: The module is automatically installed when you install Rainbow.
The install procedure is only required if you deleted the module in Admin all


HISTORY
Ver. 1.0 - 23. march 2003 - Jakob Hansen did this patch
Ver. 1.1 - 16. april 2003 - Updated to follow "Rainbow best practices"
Ver. 1.3 - 24. april 2003 - Moved all files to folder \DatabaseTableEdit
Ver. 1.3a- 23. july  2003 - Added try/catch to BindTables to catch Exceptions.

Issues and Known problems:
- Tested with Rainbow version 1.2.8.1924a - 24/04/2003


Module settings
---------------
DatabaseName: "Rainbow" 
ServerName: Can be "localhost", "NameOfYourSQLServer" or 123.123.123.123 
Trusted Connection: Yes/No 
UserID: User name (for sql-authentication - when Trusted Connection is No) 
Password: user password (for sql-authentication - when Trusted Connection is No) 


Requires:
TripleASP.TableEditor.dll in \bin folder

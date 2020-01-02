What should be done to make this module a lot better.

-Fix theme support.  The link buttons are too wide.  This module was
designed on Rainbow 1756.

-Improve installer support for adding supported modules.

-Streamline initial install with bulk load of supported modules.


-Use methods in DB classes to support modules with file system operations.
Right now all move/copy operations happen at the database level.  This means
when you do "copy" on the documents+pictures modules, it duplicates the 
database entry but does not duplicate the file.  If you then delete one of
the two, the file is gone and you have a broken entry.  Ideal way to fix this
requires making small changes to a lot of files. Example....

Pictures module.  
class PicturesDB
{
     public void MoveItem(int ItemID, int DestinationModuleID)
     {
         //call sproc to update ModuleID in db.
     }
     public void MoveAll(int SourceModuleID,int DestinationModuleID)
     {
	//call sproc to update all moduleID's to new moduleiD.
     }
     public void CopyItem(int ItemID,int DestinationModuleID)
     {
	//duplicate file and add random value to end to make it a unique file name
        //insert record into table.
     }
     public void CopyAll(int SourceModuleID, int DestinationModuleID)
     {
	//duplicate all files, add random values to end of files to make name unique
        //call sproc to do db update.
     }
}


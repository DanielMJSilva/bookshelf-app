   M S B A M L   `   `   `  ????P  L301203886(daniel)Lab2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:_301203886_daniel_Lab2_301203886_daniel_Lab2  5   	   *    $_301203886_daniel_Lab2.BookshelfList   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:_301203886_daniel_Lab2     5   	     ? Title$  BookshelfList??5   	   $	??450??6   $	??800??6,   .??5	      ? .+?5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 
booklistDG??5
   (   $??10,69,10,10q?6      AutoGenerateColumns$ False??6d      CanUserResizeColumns$
 True??6?      
IsReadOnly$
 True??6?   ??6<   ??  ?? Path$ 	bookshelf?   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 ISBN??6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   ?? *ISBN+  5      $ Title??6%    64   ?? *Title+  5      $ Authors??6%    66   ?? *BookAuthors+  5      $ Edition??6%    66   ?? *	Edition+  5      $ 	Publisher??6%    68   ?? *	Publisher+  5      $ 
LastAccess??6%    69   ?? *
LastAccess+  5      $ 
PageNumber??6%    69   ?? *
PageNumber+5      ?? 5   
   -   5        	 ??Name$	 newLabel??5       $??Label??6   $
??Left=?62   $??
155,29,0,0q?6M   $	??Top=?6a   ?? 5   
   -   5        $
??READ??5      $
??Left=?6    $??
670,10,0,0q?6;   $	??Top=?6O   $??41??6g   $	??120??6s   6   5      5      
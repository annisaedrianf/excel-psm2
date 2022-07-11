;#DropDB excelEasyTest

#Application excelWin
;#Application excelWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef excelWin

*Action About...
 
*Action OK

#EndIf

#IfDef excelWeb
*Action Navigation(My Details)
#EndIf

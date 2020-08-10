#############################################################################################################
#						Welcome to Database Infrastructure Project											#
#																											#
#############################################################################################################
#	This project contains the required structure from the project to connect and execute					#
#	commands to database.																					#
#																											#
#	This project represents the domain layer on DDD															#
#	This project should contains the following references:													#
#																										    #
#	This project shoud contains the following packages:														#
#	* Microsoft.EntityFrameworkCore (install-package Microsoft.EntityFrameworkCore)							#
#	* Microsoft.EntityFrameworkCore.Design (install-package Microsoft.EntityFrameworkCore.Design)			#
#	* Microsoft.EntityFrameworkCore.SqlServer (install-package Microsoft.EntityFrameworkCore.SqlServer)		#
#	* Microsoft.EntityFrameworkCore.Tools (install-package Microsoft.EntityFrameworkCore.Tools)				#
#	* Microsoft.Extensions.DependencyInjection (install-package Microsoft.Extensions.DependencyInjection)	#
#	* Newtonsoft.Json (install-package Newtonsoft.Json)														#
#																											#
#############################################################################################################
#																											#
#	To add objects (tables, columns, or index) this project use the migration pattern						#
#	This project use the ORM Entity Framework Core:															#
#	* First add the application context																		#	
#	* Add the objects configurations																		#
#	* Run the migration with the follow steps on Package Manager Console									#
#																											#
#############################################################################################################
#																											#
#		$env:ASPNET_CORE_DoceGrao_Database_Infrastructure_MIGRATION=                                        #
#		"Server=DESKTOP-62S01SF\SQLEXPRESS2016;Database=DoceGrao;                                           #
#		Persist Security Info=True;User ID=sa;Password=Teste@123"                                           #
#		* execute $env:ASPNET_CORE_Template_Database_Infrastructure_MIGRATION="connectionstring"			#
#		where Template_Database_Infrastructure = AssemblyName.Replace(".", "_")								#
#		this set the variable used to access the database connection string									#
#		* add-migration MigrationName																		#
#		this add the migration file structure with the commands that the will be applied on the database	#
#		* execute $env:ASPNET_CORE_Template_Database_Infrastructure_MIGRATION="connectionstring"			#
#		where Template_Database_Infrastructure = AssemblyName.Replace(".", "_")								#
#		this set the variable used to access the database connection string									#
#		* update-database																					#
#		execute the command on database																		#
#																											#
#############################################################################################################
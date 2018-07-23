Entity Framwork ::
	use concept of convenient over configuration
	Ex: when Declare property as string it take string with maxLegth nullable 
		you can change it by add annotation


Good Notes ::
	AS a Software Engineering you need to ask yourself what is the cost of the this soluation and what is the benefits


Repository :
	like domain method on memory 
	Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects.
Repository Pattern ::
	is one of the most popular patterns to create an enterprise level application. It restricts us to work directly with the data in the application and creates new layers for database operations, business logic, and the application's UI

unit of work ::
	 Unit of Work design pattern does two important things: first it maintains in-memory updates and second it sends these in-memory updates as one transaction to the database.

	So to achieve the above goals it goes through two steps:

		It maintains lists of business objects in-memory which have been changed (inserted, updated, or deleted) during a transaction.
		Once the transaction is completed, all these updates are sent as one big unit of work to be persisted physically in a database in one go.


Repository :		unit of worke 
	- get()				-complete()
	- remove() 
	- update ()
	- Add()
					
	

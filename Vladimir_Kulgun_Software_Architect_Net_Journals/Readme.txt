1. PREREQUISITES
   
   Visual Studio 2015
   Microsoft SQL Express LocalDb v11.0
   .Net 4.5.2
   IIS Express   


2. HOW TO CREATE AND INITIALIZE THE DATABASE
   
   DB will be created and populated with test data on first run
   
   As public user login for desktop client Petrov

	
3. ASSUMPTIONS
  Desktop client should not allow copying journal's content in any common way 
	- Not from web/requests/responses (WebApi sends encrypted data to a client)
	- Not from their file system (Decrypted bytes are not saved to a local disk)
    - Not from client user interface (Pdf page is rendered as an image)	

4. NOT COVERED REQUIREMENTS   
  4.1 Did not protected desktop client from making screen shots

5.1 HOW TO BUILD THE SOLUTION
   
	Run Visual Studio 2015
	Open Code\Journals.sln  		    
	Build the solution. 
   

5.1 HOW TO CONFIGURE AND RUN
      
   A. Run Web Application		
	
     Run IIS and add a new website:
        Phisycal path:  Code\Journals.WebPortal
        Type:           HTTPS
        Host name:      localhost
        Port:           44300
     Browse to https://localhost:44300     
	 

   B. Run Desktop Client
   Edit configuration file Code\Journals.DesktopClient\App.config
	
	<appSettings>
    <add key="WebApiHost" value="https://localhost:44300/" />
  </appSettings>
   
     Code\Journals.DesktopClient\bin\Debug\Journals.DesktopClient.exe
     
    
6. FOUND ISSUES

I did not expect that there are no useful commercial libraries to display pdf content for WPF. Which is not a case for  Winforms. 
Since I’ve found it out only by the end of trial I decided to keep a bit of “ugly” solution based on free library PdfiumViewer. 


7. FEEDBACK

   Technical support team did not response on my request about additional time.

# Welcome to Company Project Management Repository
### This is the first Full Stack Web App using C# I make!

#### Using Technologies:
* .Net 6
* Microsoft SQL Server Database
* Web API (Back-End)
  + RepositoryBase
  + 2 Layers Back-End (Should be 3 when using in Production)
  + Entity-Framework Code-First Database
  + Odata 8
  + JWT Token
  + Swagger Documents
* Web App (Front-End)
  + Razor Page
  + JavaScript

### What I have learned through this project:
* Using Odata for Quering Data rather than traditional API
  + While it might not be as effective or I might have not harnessed all the advantages of Odata. It was fun when learning new technology
* Using Razor Page handler method to call APIs from the Back-End
  + Also might not be an effective way to call APIs. If there's another project, I could try using AJAX to call APIs
* Using JavaScript on multiple Razor Pages
  + Most noteworthy is the use to generate extra fields and Computed Property Name to set object contains {[key]: value,...}

## How to run the Project
### You must have Microsoft SQL Server Database and .Net 6 SDK to run this project
1. Fork or clone the Project
2. Set Start-up Project as **PRN231_PE_Trial_API**
3. Go to the **Package Manager Console** and set Default Project as **PRN231_PE_Trial_API** (or **DAL** if the first doesn't work for step 4)
4. Run **Update-Database** so the Entity-Framework can Apply/Create the Migration down to your Database
5. Go back and reconfigure Start-up to run multiple projects: **PRN231_PE_Trial_API** and **PRN231_PE_Trial_WebUI**
6. Have fun and Good luck!

## Found an error?
### Create an issue and describe your problem (Include pictures or stack traces if possible)
I will try to fix it as soon as possible
### Or maybe you have ideas to improve this project. Fork this project and send me a pull request
Any ideas are always welcome

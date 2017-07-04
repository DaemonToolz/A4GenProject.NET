# A4GenProject.NET
## 4rth Year - Engineering School Dev. project
### Introduction
This one-week project was made for a sake of a project named "Generator Project", a kind off roleplay where the students played the "White Hats", in which a distributed architecture had to be set up. The principal goal of this .NET part was to receive ciphered files from a client, then it had to decipher it and use a Java EE structure whether to validate or not the content.

By default, the services had to be WCF-like - not ASP.NET - and the client turned out to be a WPF one. The entire system was bound to a SQL Server database in order to save things like username / passwords combination or Identity Tokens.

### Content
There is a total of 3 activeable services:
- TokenManagementService Services - Used to generate personnalized tokens, granting accesses to certain services
- Decipher Services - Used to receive the files and decrypt it
  * Includes a Fail-Over system in case of connection loss with the Java EE System
  * Includes a "follow-up" of the deciphering for a given file
  * Includes a service to stop the deciphering process either if the Java EE found something or the client decided to stop
- EmergencyMmsq Services - Propose a Recovery System in case of Java EE infrastructure failure

The WPF client is named AgentWpfClient.

### System explanation
To sum-up the project's behavior, instead of going into details, here's a story:
You are a new-comer in a huge society, IT-specialized one of course, and you have nothing - mean really nothing. Your boss gave you a **pass, on which 'Windows Token' is written**, plus a small sheet of paper with a **name and a password** on it. You decide to place the badge around your neck and the paper in your pocket. He gives you one folder named **'256B Basic Encryption through Windows'**.

+ You enter the society's Headquarters and you find two doors, kept by heavily-armed guards. Upon the left one, a board indicates **'Authentification'** while on the other one it is written **'Services Dispatcher, Restricted Accesses'**. Behind you, Two desks with numerous papers. Three stacks. The first stacks is the **Department names**, the second one ** Department Token **, the last one ** Desired Operations **.
+ You try to get through the second door but you get pushed back, the guard telling you: 'You cannot enter, unless you have **a token**, **a badge**, and ** a way to protect your information!**'
+ So you decide to pass through the **Authentication** door. The guard asks for your **credentials and you remember this small sheet of paper**. You give it then you pass through.
+ You reach a book-keeper, the **Token Manager**. He creates a small token then checks if it exists. If it does not, he **makes a copy** then **gives it to you** and adds: 'It lasts for **Five minutes**'. In fact, you can use this token five minutes only, afterwards you will have to get back to him.
+ Now you have your **Token**, you must know what you want to do. You reminded that once you enter the **Service Dispatcher room**, you must know the **Service Token** and the **Department's name** you want to **invoke**. Any way, you put your data in your folder and try to invoke an operation. To be precise, what you want to do is a simple decipher using a XOR Encryption with a 6 key, alphabetics.
+ You try pass through the authentication process then walk to a first front-desk. The staff looks at your folder and gives it back to you with 'You need a **Department name**, **Department Token** and finally a **Desired Operation**. '
+ You walk back to the entrance then take everything you need. You enter once again then you are accepted. The desk calls the Token Management Service then she tells you that your Token is valid for 00:04:49 minutes and every time you have to ask for a precise department, you must pass by her first.. The front-desk redirects you to the **Dispatcher**. The front-desk keeps the folder and gives you the data.
+ Now, the **Dispatcher** does a deeper job than the front-desk. This department checks your information - **Department Token** , **Department name** and **Desired Operation** - then, if these are corrects, redirects you to the **Department** you want to perform the **Desired Operation**. 
+ You move to the **Deciphering Services**, notice **a counter** above the door's frame, then enter and you present your documents. **An Old Archivist** reads it: **'Ciphered content, Algorithm, your email, Key Size...'** and he starts working. He **generates keys while deciphering the content**. He **writes** on **several papers** then **give them to his assistants**, to transmit to the **neighboor building (Java EE Infrastructure)**, then he asks you to wait outside the room. Sometimes, he loses contact so he stacks them near the **back door**, but he realized he could only save **one thousand**. Once the **contact reestablished**, his **assistants take the messages then transport them to the neighboors**. 
+ You await for a long time while checking permanently the counter. After a while, the Deciphering Service receives a call from a -**neighboor building (Java EE Infrastructure)**. Your deciphering is **completed and validated**. He asks you to enter again then gives you the results. **An email**, **the deciphered content** and the **trust index**.

This is how the overall structure works. To give a technical aspect:
+ You have to be connected to the Token Management System, using a Username/Password combinations for credentials. This service will first check into a SQL Server database if your credentials exist, if not it will deny you access. Otherwise it will generate a token then check its existence in a second database. If this token is valid, it will create a copy then provides one to you. The expiration time is set up to 00:05:00 minutes. By default, the connection type is WsHttpBinding - Http using WS\-\* Stardards. The remote service will provide a certificate as an element of trust.
+ Once you have passed the authentication process. You must prepare four elements : 
 1. A Service token - unique for a service
 2. A function name - Belonging to the service you want to invoke
 3. A user token - provided by the Token Managent service
 4. The content you must transmit, in general in an object called ConnectionToken.
 
 If the service token does not exist or the function does not belong to the service related to the token, you are ejected. When you provide the User Token, it will directly calls the Token Management Service to ask for a validation of the token. If it expires, you are denied.
+ Once you are done with this authentication thingy, you must create a net.tcp connection with a Windows Authentication and a Basic Message Encryption using a 256B algorithm.
+ You give everything to the server

In the code-behind, the server will treat the data. It will call the desire function - as for an example, Deciphering Service. This service is directly bound to a Java EE infrastructure that will validate the Decryption. If it is, then a service breaker will be called and will immediately stop the deciphering for your document, otherwise it will end up with a timeout.

Throught the WPF client, you can monitor the progress of your deciphering thanks to a WS Dual Http Binding, with a callback function.

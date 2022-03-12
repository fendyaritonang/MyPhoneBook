# MyPhonebook

MyPhonebook is a binary application for a user to record their contacts. 

## User Features

- Add new contact
- Modify a contact
- Delete a contact
- Find contact from the list

## Technical Notes
- Contact data is saved to XML file which can be configured through app.config. The default location is at the same folder with application executable file, and the default file name is phonebook.xml
- Contact data is always saved as encrypted text in XML file
- The application is using NLog to log errors and other activities. NLog configuration can be configured through app.config (e.g. log location, log name, etc)
- A unit test project is included. Scope of the testing currently is encryption and contact data operation such as create, read, update, and delete

## Running the application

1. Open the project using visual studio
2. Verify configuration value in app.config and ensure it fits your environment. There are 2 configs in app.config that you need to verify, they are the location and the name of XML file which will host contact data, and NLog configuration settings
3. Run the application

```
Created by Nurdin Effendi
```
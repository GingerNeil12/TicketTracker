# Introduction

## *Purpose of Document*

This is a Requirements Specification document for a new Ticket Trakcing system meant as a home project. Ticket Tracking systems are widely used in a number of industries to help track issues or problems within a company. This document will describe the scope, objectives and goal of the new system. In addition to describing non-functional requirements, this document models functional ones with some Use Case examples, diagrams and class models. Some of these will be referenced as another file as well. This document is intended to direct the design and implementation of the target system in an object orientated language. (C#).

<br/>

## *Project Summary*

**Project Name**: Ticket Tracker

**Project Manager**: Neil Earlam, Luke Norman

**Project Analysts**: Neil Earlam, Luke Norman

**Responsible Users**: Neil Earlam, Luke Norman, Any one else

<br/>

## *Background*

Ticket Tracking software is used the world over to help co ordinate everything from IT Support issues, Bug tracking in software and also project management. Ticket Trackers such as Jira, Asana and ZenDesk are some examples. This project hopes to emulate it and make our own custom solution. 

Currently there are several ticket tracking systems on the market that have similar capabilities, most of them also have extensive teams and other options available as a whole suite of tools for project management. 

This project aims to create a standalone Ticket Tracking system that does not require the user to have intimate knowledge of project management or agile practices. It will also not require the user to be part of a massive suite of projects and will allow the user complete control of their own tickets and data stored within.

<br />

## *Project Scope*

The scope of the project will be a web based cloud backed ticket tracker that supports users for a multitude of reasons. Ticket Tracking, User Access Control and Data Management will be at the heart of the project.

<br />

## *System Purpose*

### *Users*

Those who will primarily benefit from the new system and thos who will be affected by it:

- Project Managers
  - Upon entring a ticket into a system, PMs should find tracking or creating a ticket easy as possible.
- Administrators
  - Administrators should be able to deal with User Management in the simplest way to them. Be that on boarding new people to the board or removing ones.
- Technicians
  - Technicians should be able to see the tickets they have assigned and also enter any and all information on to them that makes things as easy as possible for working through them. 
- Reporters
  - The people reporting a ticket should be able to locate and create a ticket with as little steps as possible, but gathering the most amount of data as they can. They should be able to easily identify previous tickets they have raised and see the progress that their ticket has made through the system.
  
### *Location*

The system will be available to anyone in the world that has an internet connection. Users should be able to to see any access restricted data via use of an email and password.

### *Responsibilities*

The primary responsibilities of the system will be:

- allow users to enter a new ticket onto the system
- provide users with direct access to previous tickets to see their status and add any additional information onto it
- allow access to different parts of the site depending on the users permissions
- provide a project manager or board manager the ability to control the data held on their boards 
- provide the administrator with the ability to manage the users on the system

Other desired features are:

- a consistent look and feel to the system
- filter options on all the pages a user has access to that would require them
- help with navigation
- translation of parts to another language for teams that are based in different locations

### *Need*

This system is needed in order to help focus our skills and learn new technologies.

<br />

# Functional Objectives

## *High Priority*

1. The system shall allow users to log a ticket for an issue.
2. The system shall allow users to attach whatever information they deem necessary to that ticket.
3. The system shall allow users to update the status of a ticket.
4. The system will display all the tickets that a user has the permission to view.
5. The system will allow users to register and login.

## *Medium Priority*

1. The system will allow for filtering tickets:
   1. by keyword
   2. by title
   3. by due date
   4. by category
2. The system will allow for user management

## *Low Priority*

1. The system will allow for users to be auto logged in on remembered browsers.
2. The system will allow for users to customise their landing page
3. The system will log how the users uses the site in order to better perfect the workflow for the users.

# Non Functional Objectives

## *Reliability*

- The system should be operational for most of the year
- Downtime should be kept to a minimum

## *Usability*

- A users should be able to use the system with little training or instruction needed
- A user who already knows how to use the system should be able to navigate to what they are finiding quickly
- The amount of pages needed for users to access the ticket information should remain low 

## *Performance*

- The system should be able to handle large amount of similtaneous users.
- The loading time of the web pages should be low as possible
- Any downloads the users should be able to select the format and should be able to download as quick as possible

## *Security*

- The system shall provide password protected access to secure pages
- Data transfered should be over HTTPS for security

## *Supportability*

- The system should be able to support all modern browsers without need to re-engineer major components
- The system should be able to support those on a mobile device

## *Online Documentation and Help*

- The system should provide the ability for users to view how to use the site. The documents should be customized based on what the user is trying to do
- The help page should be accessible from all pages

## *Purchased Components*

- AWS account which is currently free tier

## *Interfaces*

The system needs to be able to interface with the following technologies:

- AWS EC2 Instance
- AWS RDS/Sql Server instance
- AWS S3 Bucket

<br />

# The Context Model

## *Goal Statement*

The goal of the system is to allow the user to easily track their tickets by:
- allowing easily access to filter and searching results and adding new tickets
- providing users with quick and easy to understand results

## *System Externals*

- Visitor
  - A Visitor is anyone who has not logged into the site. They have access to all public parts of the site and can sign up for newsletters or anything else that gets implemented for marketing.
- User
  - A user is anyone who is authenticated by the site. As a base level a user can create a ticket and update it as needed
- Project Manager
  - A project manager is anyone who is in charge of a board or grouping of tickets. They will also be able to assign or direct users to work on specific tickets
- Administrator
  - An administrator is anyone who can manage the users of a given board. They can also run any reports required.













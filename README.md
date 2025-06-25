# SkyRim
Online Learning Platform
Project Overview
Welcome to the Online Learning Platform! This project is a foundational web application built with ASP.NET Core MVC, designed to serve as a robust and secure environment for managing and delivering educational content. It focuses on core functionalities such as user authentication, course management, and the intricate relationships between users and learning materials.

Features Developed
So far, the project has successfully implemented the following key features:

- Data Models for Core Entities:

  -  Course Class: Defines the structure for educational courses, including properties like Name, Description, Author, Duration, Date (creation date), and ImageUrl.

  - Lesson Class: Represents individual lessons within a course, featuring Title, Content (as a string for text/HTML), and Order for sequencing, with a one-to-many relationship with Course.

  - UserDetails Class: A separate table for extended user profiles, including FirstName, LastName, MiddleName (optional), Region, State, Country, LinkedInUrl, and GitHubUrl, maintaining a one-to-one relationship with IdentityUser.

  - UserCourse Link Table: A many-to-many join table (UserCourse) connecting IdentityUser and Course, allowing users to enroll in multiple courses and courses to have multiple enrolled users.

- Database Context Configuration:

  - The ApplicationDbContext has been meticulously updated to correctly incorporate all custom models (Course, Lesson, UserDetails, UserCourse).

  - Entity Framework Core's Fluent API has been used to precisely define the complex relationships (one-to-many, many-to-many, one-to-one) between these entities and the default IdentityUser.

- Database Management with Migrations:

  - Various Entity Framework Core migrations have been created and applied to establish and evolve the database schema, reflecting all defined models and their relationships.

  - A SQLite database (app.db) is used for local development and persistence of all user details (managed by Identity) and course-related data.

- User Interface (UI) Development:

  - Authentication Flow: Fully functional login and registration pages are set up using ASP.NET Core Identity's default UI, ensuring secure user authentication.

  - Layout Redesign: The application's overall layout page (_Layout.cshtml) has been customized to provide a consistent and improved visual experience across the platform.

  - Landing Page: A foundational landing page provides the entry point for the application.

Technologies Used
- Backend: ASP.NET Core MVC

- Language: C#

- Database: SQLite (for development)

- ORM: Entity Framework Core

- Authentication: ASP.NET Core Identity

- Frontend Framework/Library (for UI): Bootstrap (for responsive design and styling)

Setup and Installation
To run this project locally, follow these steps:

1. Clone the repository:

git clone [Your-GitHub-Repo-URL]
cd [Your-Project-Name]

2. Open in Visual Studio:
Open the .sln file in Visual Studio (2022 or newer recommended).

3. Restore NuGet Packages:
Visual Studio should automatically prompt you to restore NuGet packages. If not, right-click on the solution in Solution Explorer and select "Restore NuGet Packages."

4. Update Database Migrations:
Open the Package Manager Console in Visual Studio (Tools > NuGet Package Manager > Package Manager Console) and run the following commands to ensure your database schema is up-to-date:

Update-Database

Note: If you encounter issues, you may need to delete your existing app.db file and the Migrations folder, then run Add-Migration Initial followed by Update-Database.

5. Run the application:
Press F5 or click the "Run" button in Visual Studio to start the web application.

Usage
- Navigate to the /Account/Register endpoint to create a new user account.

- Use the /Account/Login endpoint to sign in.

- Explore the landing page and observe the custom layout.

- (Further usage instructions will be added as more features are developed.)

Future Plans
The project is continuously evolving. Here are some of the planned features and enhancements:

- Course Content Management:

Implement CRUD (Create, Read, Update, Delete) functionality for Courses and Lessons.

Develop a rich text editor integration for Lesson content.

- User Dashboard:

Create a personalized dashboard for enrolled users to track their progress and access their courses.

- Enrollment Management:

Allow users to enroll/unenroll from courses via the UI.

Display enrolled courses on the user's profile.

- Admin Panel:

Implement an administrative interface for managing users, roles, courses, and lessons.

- Search and Filtering:

Add functionality to search for courses and filter them by various criteria.

- Deployment:

Prepare the application for deployment to a hosting environment (e.g., Azure, AWS, Heroku).

- Improved UI/UX:

Further refine the design and user experience, potentially integrating more advanced frontend techniques.

- Testing:

Implement unit and integration tests to ensure code quality and reliability.

- Feedback/Ratings:

Allow users to provide feedback or ratings on courses and lessons.

Contributing
Contributions are welcome! If you have suggestions or want to contribute, please fork the repository and create a pull request.

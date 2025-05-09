# Social Media Platform

This is a social media platform built with ASP.NET Core, providing features such as user authentication, posts, comments, likes, friend management, and more.

## Features

- **User Management**: 
  - User registration, login, and logout.
  - Update personal information and change passwords.
  - Manage user roles and contacts.

- **Posts**:
  - Create, update, delete, and view posts.
  - Upload images for posts.
  - View posts by user or category.

- **Comments**:
  - Add, update, and delete comments on posts.
  - Support for images and stickers in comments.

- **Friend Management**:
  - Send and manage friend requests.
  - View friends by categories (e.g., hometown, close friends).

- **File Uploads**:
  - Upload user avatars and post images.
  - Store files in the `wwwroot` directory.

- **Real-Time Features**:
  - Support for real-time notifications and messaging (via SignalR).

## Project Structure

- **Controllers**: Contains API endpoints for various features.
- **Models**: Defines the data models used in the application.
- **BAL**: Business logic layer for handling application logic.
- **DAL**: Data access layer for interacting with the database.
- **Helpers**: Utility classes and constants.
- **Migrations**: Database migrations for managing schema changes.
- **Pages**: Razor pages for the frontend.
- **wwwroot**: Static files such as images, CSS, and JavaScript.

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Nhathao03/Social_Media_API.git
   cd social-media


### API make by Ha Nhat Hao
Name: Hà Nhật Hào
Email: nhathao212003@gmail.com
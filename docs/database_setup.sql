-- SQL Script for setting up the AI-Powered Blog Portfolio database

-- Create Admins table
CREATE TABLE Admins (
    AdminId INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create BlogPosts table
CREATE TABLE BlogPosts (
    PostId INT PRIMARY KEY AUTO_INCREMENT,
    Title VARCHAR(255) NOT NULL,
    ContentHtml TEXT NOT NULL,
    RawContent TEXT NOT NULL,
    Tags VARCHAR(255),
    MetaDescription VARCHAR(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    AdminId INT,
    FOREIGN KEY (AdminId) REFERENCES Admins(AdminId)
);

-- Create Visitors table
CREATE TABLE Visitors (
    visitor_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    message TEXT NOT NULL,
    submitted_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create AIProcessingLogs table
CREATE TABLE AIProcessingLogs (
    log_id INT PRIMARY KEY AUTO_INCREMENT,
    post_id INT,
    processing_type VARCHAR(50) NOT NULL,
    processing_result TEXT NOT NULL,
    processed_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (post_id) REFERENCES BlogPosts(post_id)
);

-- Insert initial admin user
INSERT INTO Admins (Username, PasswordHash, Email) VALUES 
('admin', '$2a$12$eImiTXuWVxfM37uY4JANjQ==', 'admin@example.com'); -- password: admin (hashed)

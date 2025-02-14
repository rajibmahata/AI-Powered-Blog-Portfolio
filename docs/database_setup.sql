-- Create Admins table
CREATE TABLE Admins (
    AdminId INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

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
    VisitorId INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Message TEXT NOT NULL,
    SubmittedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create AIProcessingLogs table
CREATE TABLE AIProcessingLogs (
    LogId INT PRIMARY KEY AUTO_INCREMENT,
    PostId INT,
    ProcessingType VARCHAR(50) NOT NULL,
    ProcessingResult TEXT NOT NULL,
    ProcessedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (PostId) REFERENCES BlogPosts(PostId)
);





INSERT INTO Admins (Username, PasswordHash, Email) VALUES 
('admin', '$2a$12$eImiTXuWVxfM37uY4JANjQ==', 'admin@example.com'); -- password: admin (hashed)

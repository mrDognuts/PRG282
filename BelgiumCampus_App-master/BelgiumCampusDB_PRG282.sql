USE master;
CREATE DATABASE BelgiumCampusDB_PRG282;
GO

USE BelgiumCampusDB_PRG282;
CREATE TABLE Modules
(
    CourseCode INT PRIMARY KEY,
    ModuleName VARCHAR(50),
    ModuleDescription TEXT,
    Links VARCHAR(555)
);
GO

CREATE TABLE Students
(
    StudentID INT IDENTITY(101,1) PRIMARY KEY,
    FirstName VARCHAR(30) NOT NULL,
    Surname VARCHAR(30) NOT NULL,
    DOB DATE NOT NULL,
    Gender VARCHAR(1) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    StudentImage IMAGE,
    CourseCode INT,
    CONSTRAINT FK_ModuleCode_StudentID FOREIGN KEY (CourseCode) REFERENCES Modules(CourseCode)
);
GO

INSERT INTO Modules VALUES 
(1001, 'Bachelor of Computing', 'Mathematics, Programming, Web Programming, Database Development, Information Systems, Machine Learning, Software Engineering, Data Science', 'https://www.belgiumcampus.ac.za/bachelor-of-computing/ , https://archive.org/details/engineeringmathe0000stro , https://openlibrary.org/books/OL24576701M/Information_technology_research , https://www.cambridge.org/core/books/practical-data-science-for-information-professionals/7286D504BEA81B1367EF67B71698182F'),
(1002, 'Bachelor of Information Technology', 'Mathematics, Programming, Web Programming, Database Development, Cloud-Native Application Architecture, Ethics, Enterprise Systems, Data Analytics', 'https://www.belgiumcampus.ac.za/bachelor-of-information-technology/ , https://openlibrary.org/books/OL8805343M/IT_Ethics_Handbook , https://skillexperto.com/index/mysap-erp-for-dummies/ , https://skillexperto.com/index/mysap-erp-for-dummies/'),
(1003, 'Diploma in Information Technology', 'Mathematics, Business Communication, Business Management and Entrepreneurship, Database Concept, Database Development, Enterprise Systems, IT Law and Ethics, Project Management', 'https://www.belgiumcampus.ac.za/diploma-in-information-technology/ , https://epdf.tips/database-modeling-and-design-logical-design-5ea6a80ac2de0.html , https://openlibrary.org/books/OL8805343M/IT_Ethics_Handbook , https://www.taylorfrancis.com/books/edit/10.4324/9780203793961/current-topics-management-robert-golembiewski');
GO

INSERT INTO Students ( FirstName, Surname, DOB, Gender, Phone, CourseCode) VALUES
('Morgan','Pearson','2001-09-03','M','082-023-5559', 1001),
('Ella', 'Johnson', '1999-07-15', 'F', '076-321-9876', 1001),
('Sam', 'Nguyen', '2003-04-28', 'M', '064-555-1234', 1002),
('Sophia', 'Van Der Merwe', '2000-11-20', 'F', '083-456-7890', 1003);
GO


CREATE TABLE StudentAddresses
(
    AddressID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT,
    Street VARCHAR(100),
    City VARCHAR(50),
    State VARCHAR(50),
    Country VARCHAR(50),
    PostalCode VARCHAR(20),
    CONSTRAINT FK_StudentAddresses_StudentID FOREIGN KEY (StudentID) REFERENCES Students(StudentID)
);
GO

INSERT INTO StudentAddresses (StudentID, Street, City, State, Country, PostalCode) VALUES
(101, '123 Main St', 'Pretoria', 'Gauteng', 'South Africa', '0002'),
(102, '456 Oak Ave', 'Johannesburg', 'Gauteng', 'South Africa', '2001'),
(103, '789 Pine St', 'Cape Town', 'Western Cape', 'South Africa', '8001'),
(104, '321 Maple Ave', 'Durban', 'KwaZulu-Natal', 'South Africa', '4001');
GO

/*Stored proc*/
USE BelgiumCampusDB_PRG282;
GO
/*Display Student*/
CREATE PROCEDURE GetStudentInfo
AS
BEGIN
    SELECT 
        S.StudentID, 
        S.FirstName, 
        S.Surname, 
        S.DOB, 
        S.Gender, 
        S.Phone, 
        S.StudentImage, 
        A.Street, 
        A.City, 
        A.State, 
        A.Country, 
        A.PostalCode,
        S.CourseCode
    FROM Students S
    INNER JOIN StudentAddresses A ON S.StudentID = A.StudentID;
END;
GO

--EXEC GetStudentInfo;

/*Delete Student*/
CREATE PROCEDURE spDeleteStudent
    @stdID INT
AS
BEGIN
    DELETE FROM StudentAddresses WHERE StudentID = @stdID;
    DELETE FROM Students WHERE StudentID = @stdID;
END
GO
--DROP PROCEDURE spDeleteStudent
/*Delete module*/
CREATE PROCEDURE spDeleteModule
    @CourseCode INT
AS
BEGIN
    DELETE FROM Modules WHERE CourseCode = @CourseCode;
END
GO

/*Get Student image*/
CREATE PROCEDURE spGetStudentImage
    @stdID INT
AS
BEGIN
    SELECT StudentImage
    FROM Students
    WHERE StudentID = @stdID;
END
GO

CREATE PROCEDURE spUpdateImage
@Image Image,
@StudentID INT
AS
BEGIN
	UPDATE Students 
	SET StudentImage = @Image 
	WHERE StudentID = @StudentID;
END
GO

/*Add student*/
CREATE PROCEDURE spAddStudent
    @FirstName VARCHAR(30),
    @Surname VARCHAR(30),
    @DOB DATE,
    @Gender VARCHAR(1),
    @Phone VARCHAR(20),
    @CourseCode INT
AS
BEGIN
    INSERT INTO Students (FirstName, Surname, DOB, Gender, Phone, CourseCode)
    VALUES (@FirstName, @Surname, @DOB, @Gender, @Phone, @CourseCode);
END;
GO

/*Add student address*/
CREATE PROCEDURE spAddStudentAddress
    @street VARCHAR(100),
    @city VARCHAR(50),
    @prov VARCHAR(50),
    @country VARCHAR(50),
    @postalCode VARCHAR(20)
AS
BEGIN
    INSERT INTO StudentAddresses (StudentID, Street, City, State, Country, PostalCode)
    SELECT TOP 1 StudentID, @street, @city, @prov, @country, @postalCode
    FROM Students
    ORDER BY StudentID DESC;
END
GO

--DROP PROCEDURE spAddStudentAddress

CREATE PROCEDURE spGetStudentID
AS
BEGIN
	SELECT TOP 1 StudentID FROM Students ORDER BY StudentID DESC;
END
GO

/*Add new module*/
CREATE PROCEDURE spAddModule
    @courseCode INT,
    @moduleName VARCHAR(50),
    @moduleDesc TEXT,
	@link VARCHAR(555)
AS
BEGIN
    INSERT INTO Modules (CourseCode, ModuleName, ModuleDescription, Links)
    VALUES (@courseCode, @moduleName, @moduleDesc, @link);
END
GO

/*Update module*/
CREATE PROCEDURE spUpdateModulesData
    @moduleID INT,
    @newModuleName VARCHAR(50),
    @newModuleDescription TEXT,
	@link VARCHAR(555)
AS
BEGIN
    UPDATE Modules
    SET ModuleName = @newModuleName, ModuleDescription = @newModuleDescription, Links = @link
    WHERE CourseCode = @moduleID;
END
GO

/* Update Student*/
CREATE PROCEDURE spUpdateStudent
    @StudentID INT,
    @FirstName VARCHAR(30) = NULL,
    @Surname VARCHAR(30) = NULL,
    @DOB DATE = NULL,
    @Gender VARCHAR(1) = NULL,
    @Phone VARCHAR(20) = NULL,
    @CourseCode INT = NULL
AS
BEGIN
    UPDATE Students
    SET FirstName = ISNULL(@FirstName, FirstName),
        Surname = ISNULL(@Surname, Surname),
        DOB = ISNULL(@DOB, DOB),
        Gender = ISNULL(@Gender, Gender),
        Phone = ISNULL(@Phone, Phone),
        CourseCode = ISNULL(@CourseCode, CourseCode)
    WHERE StudentID = @StudentID;
END;
GO


/*Update StudentAddress*/
CREATE PROCEDURE spUpdateStudentAddress
    @stdID INT,
    @newStreet VARCHAR(100),
    @newCity VARCHAR(50),
    @newProvince VARCHAR(50),
    @newCountry VARCHAR(50),
    @newPostalCode VARCHAR(20)
AS
BEGIN
    UPDATE StudentAddresses
    SET Street = @newStreet, City = @newCity, State = @newProvince, Country = @newCountry, PostalCode = @newPostalCode
    WHERE StudentID = @stdID;
END
GO
--DROP PROCEDURE spUpdateStudentAddress
/*Search Student*/
CREATE PROCEDURE spSearchStudent
    @StudentID INT
AS
BEGIN
    SELECT 
        S.StudentID, 
        S.FirstName, 
        S.Surname, 
        S.DOB, 
        S.Gender, 
        S.Phone, 
        S.StudentImage, 
        A.Street, 
        A.City, 
        A.State, 
        A.Country, 
        A.PostalCode,
        S.CourseCode
    FROM Students S
    INNER JOIN StudentAddresses A ON S.StudentID = A.StudentID
    WHERE S.StudentID = @StudentID;
END;
GO


/*Search module*/
CREATE PROCEDURE spSearchModule
    @courseID INT
AS
BEGIN
    SELECT *
    FROM Modules
    WHERE CourseCode = @courseID;
END



--DROP DATABASE BelgiumCampusDB_PRG282
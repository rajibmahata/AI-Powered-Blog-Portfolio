# API Structure for AI-Powered Blog Portfolio Backend

## Base URL
The base URL for all API endpoints is `/api`.

## Endpoints

### 1. Admin Authentication
- **POST /api/admin/login**
  - **Description:** Authenticate an admin user.
  - **Request Body:**
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```
  - **Response:**
    ```json
    {
      "token": "string"  // JWT token for authentication
    }
    ```

### 2. Blog Management
- **GET /api/blogs**
  - **Description:** Retrieve a list of all blog posts.
  - **Response:**
    ```json
    [
      {
        "post_id": "integer",
        "title": "string",
        "content_html": "string",
        "tags": "string",
        "meta_description": "string",
        "created_at": "timestamp",
        "updated_at": "timestamp"
      }
    ]
    ```

- **GET /api/blogs/{id}**
  - **Description:** Retrieve a specific blog post by ID.
  - **Response:**
    ```json
    {
      "post_id": "integer",
      "title": "string",
      "content_html": "string",
      "tags": "string",
      "meta_description": "string",
      "created_at": "timestamp",
      "updated_at": "timestamp"
    }
    ```

- **POST /api/blogs**
  - **Description:** Create a new blog post with raw content.
  - **Request Body:**
    ```json
    {
      "title": "string",
      "raw_content": "string",
      "admin_id": "integer"
    }
    ```
  - **Response:**
    ```json
    {
      "post_id": "integer",
      "title": "string",
      "content_html": "string",
      "tags": "string",
      "meta_description": "string",
      "created_at": "timestamp",
      "updated_at": "timestamp"
    }
    ```

- **PUT /api/blogs/{id}**
  - **Description:** Update an existing blog post by ID.
  - **Request Body:**
    ```json
    {
      "title": "string",
      "raw_content": "string"
    }
    ```
  - **Response:**
    ```json
    {
      "post_id": "integer",
      "title": "string",
      "content_html": "string",
      "tags": "string",
      "meta_description": "string",
      "created_at": "timestamp",
      "updated_at": "timestamp"
    }
    ```

- **DELETE /api/blogs/{id}**
  - **Description:** Delete a blog post by ID.
  - **Response:**
    ```json
    {
      "message": "Blog post deleted successfully."
    }
    ```

### 3. Visitor Inquiries
- **POST /api/visitors/contact**
  - **Description:** Submit an inquiry through the contact form.
  - **Request Body:**
    ```json
    {
      "name": "string",
      "email": "string",
      "message": "string"
    }
    ```
  - **Response:**
    ```json
    {
      "message": "Inquiry submitted successfully."
    }
    ```

### 4. AI Processing Logs
- **GET /api/ai/logs**
  - **Description:** Retrieve a list of AI processing logs.
  - **Response:**
    ```json
    [
      {
        "log_id": "integer",
        "post_id": "integer",
        "processing_type": "string",
        "processing_result": "string",
        "processed_at": "timestamp"
      }
    ]
    ```

- **GET /api/ai/logs/{id}**
  - **Description:** Retrieve a specific AI processing log by ID.
  - **Response:**
    ```json
    {
      "log_id": "integer",
      "post_id": "integer",
      "processing_type": "string",
      "processing_result": "string",
      "processed_at": "timestamp"
    }
    ```

## Security
- All admin endpoints require authentication using JWT tokens.
- Token should be included in the `Authorization` header as a Bearer token.

Example:
```plaintext
Authorization: Bearer <token>
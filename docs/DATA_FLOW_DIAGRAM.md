# Data Flow Diagram for AI-Powered Blog Portfolio

## Level 0: Context Diagram

1. **Admin**
   - **Input:** Raw blog content
   - **Output:** Refined blog content, AI-generated title, tags, and SEO metadata

2. **Visitor**
   - **Input:** Requests to view blogs
   - **Output:** Displays blog listings and detailed blog content

3. **External AI Service (e.g., OpenAI API)**
   - **Input:** Raw blog content for processing
   - **Output:** Refined content, title, tags, and SEO metadata

4. **Database (MySQL)**
   - **Input:** Refined blog content, metadata
   - **Output:** Stored blog posts for frontend display

## Level 1: Detailed Diagram

### Admin Panel

1. **Login System**
   - **Input:** Admin credentials
   - **Output:** Access to admin panel

2. **Blog Management**
   - **Create Blog Post**
     - **Input:** Raw blog content
     - **Process:** Send content to AI service
     - **Output:** Receive refined content, title, tags, and SEO metadata
     - **Store:** Save refined blog content and metadata in MySQL

   - **Edit Blog Post**
     - **Input:** Blog post ID, new content
     - **Process:** Send content to AI service
     - **Output:** Receive refined content, update blog post in MySQL

   - **Delete Blog Post**
     - **Input:** Blog post ID
     - **Process:** Remove blog post from MySQL

### AI Service

1. **Content Refinement**
   - **Input:** Raw blog content
   - **Output:** Refined content

2. **Title Generation**
   - **Input:** Raw blog content
   - **Output:** Engaging title

3. **Tagging**
   - **Input:** Raw blog content
   - **Output:** Relevant tags

4. **SEO Optimization**
   - **Input:** Raw blog content
   - **Output:** SEO metadata (meta descriptions, keywords)

### Frontend (Blazor)

1. **Home Page**
   - **Input:** Request for featured blogs
   - **Output:** Display featured blogs

2. **Blog Listing Page**
   - **Input:** Request for blog listings
   - **Output:** Display list of blogs

3. **Blog Detail Page**
   - **Input:** Request for specific blog post
   - **Output:** Display detailed blog content

4. **Contact Us Page**
   - **Input:** User inquiries
   - **Output:** Send inquiries to admin

### Database (MySQL)

1. **Store Blog Posts**
   - **Input:** Refined blog content, metadata
   - **Output:** Stored blog posts

2. **Retrieve Blog Posts**
   - **Input:** Request for blog content
   - **Output:** Blog content for frontend display
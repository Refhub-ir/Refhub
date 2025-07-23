 ```mermaid
sequenceDiagram
  participant User
  participant Frontend
  participant Backend
  participant Meilisearch
  User->>Frontend: وارد کردن عبارت جستجو
  Frontend->>Backend: ارسال Query جستجو
  Backend->>Meilisearch: جستجو در ایندکس منابع
  Meilisearch-->>Backend: نتایج جستجو
  Backend-->>Frontend: ارسال نتایج
  Frontend-->>User: نمایش نتایج و فیلترها
```

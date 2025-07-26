 ```mermaid
sequenceDiagram
  participant User
  participant Frontend
  participant Backend
  participant PaymentGateway
  User->>Frontend: افزودن به سبد خرید و ثبت سفارش
  Frontend->>Backend: ارسال اطلاعات سفارش
  Backend->>PaymentGateway: درخواست پرداخت
  PaymentGateway-->>Backend: تایید/خطای پرداخت
  Backend-->>Frontend: نمایش نتیجه پرداخت
  Frontend-->>User: نمایش پیام موفقیت یا خطا
```

 # مستندات Middlewares پروژه

در این پروژه چندین میدلور اختصاصی برای مدیریت رفتار درخواست‌ها و امنیت سیستم پیاده‌سازی شده است:

---

## 1. UserActivityMiddleware
- **مسیر:** RefHubWeb/middleware/base_middleware.py
- **کاربرد:** ثبت فعالیت‌های کاربران لاگین‌شده (view، method، path، پارامترها) در دیتابیس برای تحلیل رفتار و امنیت.

## 2. DefaultLanguageMiddleware
- **مسیر:** RefHubWeb/middleware/base_middleware.py
- **کاربرد:** حذف پیشوند زبان پیش‌فرض (مثلاً /en/) از URL و ریدایرکت به مسیر بدون پیشوند.

## 3. RemoveWwwMiddleware
- **مسیر:** RefHubWeb/middleware/base_middleware.py
- **کاربرد:** حذف www از ابتدای دامنه و ریدایرکت به دامنه بدون www. همچنین ریدایرکت دامنه‌های خاص به دامنه اصلی.

## 4. LanguageRedirectMiddleware
- **مسیر:** RefHubWeb/middleware/base_middleware.py
- **کاربرد:** ریدایرکت کاربران به زبان مناسب بر اساس کوکی یا IP (مثلاً کاربران ایران به /fa/). تشخیص کشور با ip3country.

## 5. Log404Middleware
- **مسیر:** RefHubWeb/middleware/log_404_middleware.py
- **کاربرد:** ثبت لاگ خطاهای 404 و 500 (و جزئیات خطا) در Sentry برای تحلیل و رفع مشکلات.

## 6. RateLimitMiddleware
- **مسیر:** RefHubWeb/middleware/rate_limit.py
- **کاربرد:** محدودسازی تعداد درخواست‌های هر IP در بازه زمانی (مثلاً 60 درخواست در دقیقه، 1000 در ساعت). جلوگیری از حملات و سوءاستفاده. کراولرهای معتبر و IPهای سفید مستثنی هستند.

---

> **نکته:** ترتیب قرارگیری میدلورها در settings.py اهمیت دارد و برخی میدلورها فقط برای کاربران لاگین یا مسیرهای خاص فعال هستند.

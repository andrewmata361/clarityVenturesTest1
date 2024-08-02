Clarity test.

Project makes use of asp.NET Core, EntityFramework(inMemory) and React(for Frontend)

As requested, the mailing functions are located in a DLL and injected through dependancy reference (Also included in /lib/ in case it was showing up on init.
I wrote the working functionality for mail functions, although I commented them out and made use of a mock system for ease of use during the test. So all of that code is still visible and located 
within the mailer dll

Api is also accessible via Postman and has swagger included for ease of use / requests in test. For the test, I did not include any special authentication systems or throttling / ratelimiting.

Overall I believe all points have been hit including extra credit options.


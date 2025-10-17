Framework test contains one test that performs a one line GET function. This highlights the easy of use of the framework.
Once the routes are setup and the framework is customized to the needs of the project, test writing can be quick and easy.

Testing.Common is part of the framework that supports the test. This is the meat of the framework.
However, it does not contain several features that I have available to use such as, the TimeBuilder which allows for easy time conversions between
time and time offsets to UTC taking in account of timezones and JWT auth token builder to name a few.

TestApiService is just that, a quick endpoint to run the test framework against.

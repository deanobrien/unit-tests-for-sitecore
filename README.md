## Unit Tests for Sitecore
This is a demo project that showcases some approaches to unit testing for sitecore projects. Inspiration for alot of it came from reading the CodefLood blog on unit testing here: https://www.codeflood.net/blog/2020/05/17/logicless-view-itemless-model/

This follows the principle discussed in the blog which are:
- Keep business logic out of the views
- Keep  `Item`  out of the model
- Encapsulate Logic
- Use Dependency Injection and Abstractions
- Avoid Implicit Data
- Use Sitecore Abstractions

The code within is written using both nSubstitute and Moq (for mocking dependencies) and uses both XUnit and MSTest (unit testing frameworks). This should allow people to pick and choose which combination they prefer for their project.
The initial comparison of the different technologies was to support an internal discusison over which would be the best fit for our own projects.
> Comparison can be seen on my blog here [DeanOBrien: Unit testing Which Framework?](https://deanobrien.uk/unit-testing-which-framework/).
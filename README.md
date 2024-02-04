Smart Delivery 

Brief:
The application was created to pass the IT project management course at Częstochowa University of Technology

![ref1]

App concept!

In the era of the current pandemic, access to stationary services to which the gray Smith was accustomed has been hampered. Faced with this problem, I propose to create an application to manage the ordering of food from restaurants. The purpose of this application will be to provide the user with an interface through which he will be able to order for himself any product from any restaurant through the described application, which makes the delivery of goods available in a simple and intuitive way. The project will be implemented with the help of Microsoft technology, namely using the .net core MVC framework, this will allow to quickly achieve a satisfactory result and provide convenience for the programmers taking an active part in the project. Among other things, the application will be based on n-layer architecture, the back-end will be created using c# language and the front-end will be based on Bootstrap css library.

Database schema

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.016.jpeg)

Following is the documentation for the Smart Delivery web application


New user registration view

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.019.jpeg)

Above is the registration view, the first account to be registered is an account with administrator privileges granted.

View of the login panel

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.020.jpeg)

Above is the login view of the administrator, he can add categories, manage users, create restaurants, add employees to restaurants.

The range of capabilities of the administrator

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.021.jpeg)

Ability to add categories of dishes

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.022.jpeg)

The buttons next to the name of a category are responsible for:

Green button - displaying detailed information about the category Yellow button - editing the category

Red button - deleting a category

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.023.jpeg)

In the details panel of a category, we can additionally add subcategories.

Ability to add new restaurants

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.024.jpeg)

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.025.jpeg)

The restaurant list offers:

- Search for a particular restaurant by name, locality
- Managing restaurants using buttons next to the name of the establishment

The details view of a particular restaurant allows you to:

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.026.jpeg)

- Adding new employees 
- Displaying the list of current employees 
- Updating restaurant data

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.027.jpeg)

Ability to block an employee's account from the employee list


Range of possibilities for an employee

Ability to add new dishes

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.028.jpeg) 

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.029.jpeg)

The restaurant's food list can be edited and updated by the employee.

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.030.jpeg)

The ability to manage and update the additional information of a given dish.


The range of possibilities for the customer

The customer, after registering his account and logging into it, will be redirected to the start screen through which he can search for the restaurant he is interested in and place an order     

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.031.jpeg)

After selecting the restaurant he is interested in, he will be redirected to the current offer of the restaurant in question.    

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.032.jpeg)

After selecting the dish he is interested in, he will be redirected to a view that allows him to add the dish to his shopping cart.             

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.033.jpeg)

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.034.png)

If the dish is successfully added in the above bar, the current status of the cart will change 

 ![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.035.jpeg)


Once in the shopping cart, we have the option to manage its contents, continue shopping or complete the order.

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.036.jpeg)

When placing an order, you will be asked to enter details of the delivery address.

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.037.jpeg)

![](./md_imgs/spose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.038.jpeg)

To successively complete the ordering phase, the user should pay for it. Payments are made using the BLIK code.

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.039.jpeg)

![](./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.040.png)

If the BLIK code is accepted by the payment processing system, the order placement will be successful and the order will proceed to the processing stage. If an incorrect BLIK code is entered, the transaction will be rejected.

[ref1]: ./md_imgs/Aspose.Words.43edbb3c-a16e-40c1-9a15-524b83f4fe29.002.png

export interface User{
    firstName:string;
    lastName:string;
    email:string;
    passwordHash:string;
    phoneNumber:string;
    address:string;
    city:string;
    country:string;
}

export interface LoginUser extends Pick<User, 'email' | 'passwordHash'> {}
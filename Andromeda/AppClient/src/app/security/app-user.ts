import { AppUserRole } from "./app-user-role";

export class AppUser {
    Username: string = "";
    Password: string = "";
    UserPassword: string = "";
    Role: AppUserRole = new AppUserRole();
}

export interface User {
    UserId: number;
    UserName: string;
    UserPassword: string;
}
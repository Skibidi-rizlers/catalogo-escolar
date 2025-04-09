import { jwtDecode } from "jwt-decode";
import { User } from "../_models/user";
export class JwtParser {
    private token: string;

    constructor(token: string) {
        this.token = token;
    }

    public getUser(): User | undefined {
        try {
            const decoded: any = jwtDecode(this.token);
            const user: User = {
                name: decoded.unique_name,
                email: decoded.email,
                role: decoded.role?.toLowerCase(),
            };

            return user;
        } catch (error) {
            console.error("Invalid JWT:", error);
            return undefined;
        }
    }
}
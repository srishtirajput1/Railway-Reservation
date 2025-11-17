export interface Customer {
    id: number;
    name: string;
    email: string;
    username: string;
    password: string;
    role: 'admin' | 'customer';
}

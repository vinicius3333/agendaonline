import { Time } from '@angular/common';

export class User {
    constructor() {}
    id: number;
    fullName: string;
    company: string;
    marketSegment: string;
    imagemPerfil: string;
    abertura: Time;
    fechamento: Time;
    duracao: Time;
    userName: string;
    email: string;
    password: string;
}

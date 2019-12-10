export class RegisterModel{
    constructor(
        public email : string,
        public password  :string,
        public firstname? :string,
        public lastname? : string
    ){}
}
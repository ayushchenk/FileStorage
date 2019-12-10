export class TokenWrap{
    constructor(
        public token : string,
        public createDate: Date,
        public expireDate : Date,
        public lifeHours : number,
        public isAdmin : boolean,
        public email : string,
        public userId : string
    ){}
}
export default interface IMessage {
    Id: string;
    Template: string;
    Email: string;
    Name: string | null;
    Body: any;
}
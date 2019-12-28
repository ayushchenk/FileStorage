import { Guid } from 'guid-typescript';

export class Folder {
    public id: string = null;
    public folderName: string;
    public userId: string;
    public parentFolderId;
}
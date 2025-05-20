import { Events } from "./Events";
import { SocialMedia } from "./SocialMedia";
import { SpeakerEvent } from "./SpeakerEvent";

export interface Speaker {
    id: number;

    name: string;

    speakerInformation: string;

    imageURL: string;

    phone: string;

    email: string;

    socialMedias?: SocialMedia[];
    
    speakerEvents?: Events[];
}
import { Lote } from "./Lote";
import { SocialMedia } from "./SocialMedia";
import { Speaker } from "./Speaker";

export interface Events {
    id: number;

    local: string;

    eventDate?: Date;

    theme: string;

    peopleCount: number;

    imageURL: string;

    phone: string;

    email: string;

    lotes: Lote[];

    socialMedias: SocialMedia[];

    speakersEvents: Speaker[];
}
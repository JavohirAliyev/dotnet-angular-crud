import { Agent } from "./agent.entity";

export interface Property {
    id: number;
    address: string;
    price: number;
    agentId: number;
}
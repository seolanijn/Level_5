import { config } from "dotenv";
config();
export const rawdata = process.env.ISOCOUNTRIES;
export const gocAlerts = process.env.GOCALERTS;

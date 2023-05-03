const schema = ` 
type Query {
    project1_setup: Result,
    alerts: [Alert],
    alertsforregion(region: String): [Alert],
    alertsforsubregion(subregion: String): [Alert],
    regions: [String],
    subregions: [String],
    travelers: [String],
    advisoriesforname(name: String): [Advisory],
 },
type Result {
 results: String
},
type Alert {
    country: String
    name: String
    text: String
    date: String
    region: String
    subregion: String
},
type Advisory {
    name: String
    country: String
    text: String
    date: String
},
type Mutation {
   addadvisory(name: String, country: String, text: String, date: String): Advisory
},
   
`;
export { schema };

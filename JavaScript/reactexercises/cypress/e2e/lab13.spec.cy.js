describe("Test Lab 13 - Load and find User From Autocomplete", () => {
  it("finds the server and builds a sentence", () => {
    cy.visit("http://localhost:5173/");
    cy.get("#menubtn").click();
    cy.contains("a", "Lab 13").click();
    cy.contains("Lab 13");
    cy.wait(1000);
    cy.contains("users loaded");
    cy.get("#users").click().type("seolan{downarrow}{enter}");
    cy.contains("Seolan Jin");
    cy.contains("sj@here.com");
  });
});

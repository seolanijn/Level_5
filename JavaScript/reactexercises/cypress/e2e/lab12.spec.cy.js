describe("Test Lab 12 Sentence Builder", () => {
  it("finds the server and builds a sentence", () => {
    cy.visit("http://localhost:5173/");
    cy.get("#words").click().type("{downArrow}{enter}");
    cy.contains("Hey");
    cy.get("#words").click().type("{downArrow}{enter}");
    cy.contains("Hey I");
    cy.get("#words").click().type("{downArrow}{enter}");
    cy.contains("Hey I built");
    cy.get("#words").click().type("{downArrow}{enter}");
    cy.contains("Hey I built a");
    cy.get("#words").click().type("{downArrow}{enter}");
    cy.contains("Hey I built a sentence.");
    cy.get("#words").click().type("{downArrow}{enter}");
    cy.contains("Hey I built a sentence. Seolan Jin");
  });
});

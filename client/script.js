async function fetchCustomers() {
  try {
    const response = await fetch("https://localhost:7115/api/customers");
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const customers = await response.json();
    const tableBody = document.querySelector("#customer-table tbody");
    tableBody.innerHTML = ""; // Clear existing rows

    customers.forEach((customer) => {
      const row = document.createElement("tr");
      const address = customer.address
        ? `${customer.address.street}, ${customer.address.city}, ${customer.address.country}`
        : "Not specified";

      row.innerHTML = `
            <td>${customer.firstName}</td>
            <td>${customer.lastName}</td>
            <td>${customer.email}</td>
            <td>${customer.phoneNumber}</td>
            <td>${address}</td>
            `;
      tableBody.appendChild(row);
    });
  } catch (error) {
    console.error("Error fetching customers:", error);
  }
}

// Fetch customers on page load
document.addEventListener("DOMContentLoaded", fetchCustomers);

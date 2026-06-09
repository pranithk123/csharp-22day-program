<script setup>
import { ref, onMounted } from 'vue'

// Reactive array that will hold patients.
const patients = ref([])
const result = ref([]) 
const name = ref('')

// Define search function at top level
async function searchPatients() {
  const response = await fetch(`https://localhost:7214/api/patients/search?name=${name.value}`)
  patients.value = await response.json()
}

async function getEncountersByDepartment() {
  const response = await fetch(`https://localhost:7214/api/analytics/department-load`)
  result.value = await response.json()
}

// Runs automatically when page loads.
onMounted(async () => {
  // Optionally load initial data (empty search)
  await searchPatients()
})

onMounted(async () => {
  await getEncountersByDepartment()
})
</script>


<template>

  <h1>CareBridge Patients</h1>

  

   <table border="1">

    <tr>
      <th>dept name</th>
      <th>inpatient</th>
      <th>outpatient</th>
      <th>ed</th>
      <th>total</th>
    </tr>

    <!-- Loop through all patients -->

    <tr
      v-for="r in result"
      :key="r.DepartmentName">

      <td>{{ r.departmentName }}</td>
      <td>{{ r.inpatient }}</td>
      <td>{{ r.outpatient }}</td>
      <td>{{ r.ed }}</td>
      <td>{{ r.total }}</td>

    </tr>

  </table>

</template>

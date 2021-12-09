<template>
  <!-- TASK -->
  <div class="border-collapse border-black rounded w-96 mt-10 m-auto">
    <button
      @click="CreateClick"
      class="border-2 border-black hover:border-gray-200 w-24 h-10 rounded"
    >
      ADD TASK
    </button>
    <table class="table-auto mt-6 w-96 text-center border rounded-md">
      <thead>
        <tr class="border border-green-800 w-24 h-10">
          <th class="border border-green-800">TASK ID</th>
          <th class="border border-green-800">TASK NAME</th>
          <th class="border border-green-800">STATUS</th>
          <th class="border border-green-800">ACTION</th>
        </tr>
      </thead>
      <tbody>
        <tr
          class="border border-green-800 w-24 h-10"
          v-for="Task in Tasks"
          :key="Task.taskId"
        >
          <td class="border border-green-800">{{ Task.taskId }}</td>
          <td class="border border-green-800">{{ Task.taskName }}</td>
          <td class="border border-green-800">{{ Task.status }}</td>
          <td class="border border-green-800 w-24">
            <button @click="EditClick(Task.taskId)" class="border-b-2">
              Edit
            </button>
            /
            <button @click="DeleteClick(Task.taskId)" class="border-b-2">
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <!-- ADD -->
  <div
    class="absolute inset-x-0 top-0 text-center"
    @click.self="closeAdd"
    v-if="showAddTask"
  >
    <div class="w-96 mx-auto mt-24 bg-white border border-black rounded-md">
      <slot>
        <from>
          <input
            type="text"
            placeholder="TASK NAME"
            class="border border-black focus:border-blue-500 rounded mb-4 mt-4 w-46 h-12 text-center"
            v-model="taskname"
          />
          <br />
          <select
            class="border border-black focus:border-blue-500 rounded mb-4 mt-4 w-48 h-12 text-center"
            v-model="status"
          >
            <option value="Doing">Doing</option>
            <option value="To-Do">To-Do</option>
            <option value="Done">Done</option>
          </select>
          <br />
          <button
            @click="AddClick"
            class="border-2 border-black hover:border-blue-500 rounded mb-4 mt-4 w-24 h-12"
          >
            ADD
          </button>
        </from>
      </slot>
    </div>
  </div>
  <!-- Edit -->
  <div
    class="absolute inset-x-0 top-0 text-center"
    @click.self="closeEdit"
    v-if="showEditTask"
  >
    <div class="w-96 mx-auto mt-24 bg-white border border-black rounded-md">
      <slot>
        <from>
          <input
            type="text"
            placeholder="TASK NAME"
            class="border border-black focus:border-blue-500 rounded mb-4 mt-4 w-46 h-12 text-center"
            v-model="taskname"
          />
          <br />
          <select
            class="border border-black focus:border-blue-500 rounded mb-4 mt-4 w-48 h-12 text-center"
            v-model="status"
          >
            <option value="Doing">Doing</option>
            <option value="To-Do">To-Do</option>
            <option value="Done">Done</option>
          </select>
          <br />
          <button
            @click="UpdateClick(taskid)"
            class="border-2 border-black hover:border-blue-500 rounded mb-4 mt-4 w-24 h-12"
          >
            Update
          </button>
        </from>
      </slot>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "Todo",
  data() {
    return {
      Tasks: [],
      taskid: "",
      taskname: "",
      status: "Doing",
      showAddTask: false,
      showEditTask: false,
      userid: this.$route.params.id,
    };
  },
  mounted() {
    axios
      .get(`http://localhost:8080/api/task/gettasks/${this.userid}`)
      .then((res) => {
        this.Tasks = res.data;
      });
  },
  methods: {
    closeAdd() {
      this.showAddTask = !this.showAddTask;
    },
    closeEdit() {
      this.showEditTask = !this.showEditTask;
    },
    CreateClick() {
      this.showAddTask = !this.showAddTask;
      if (this.showEditTask == true) {
        this.showEditTask = !this.showEditTask;
      }
      this.taskname = "";
      this.status = "Doing";
    },
    EditClick(Id) {
      this.showEditTask = !this.showEditTask;
      if (this.showAddTask == true) {
        this.showAddTask = !this.showAddTask;
      }
      axios
        .get(`http://localhost:8080/api/task/edittasks/${this.userid}/` + Id)
        .then((res) => {
          this.taskid = res.data.taskId;
          this.taskname = res.data.taskName;
          this.status = res.data.status;
        });
    },
    DeleteClick(Id) {
      if (confirm("Do you really want to delete?")) {
        axios
          .delete(
            `http://localhost:8080/api/task/deletetask/${this.userid}/` + Id
          )
          .then(() => {
            this.$router.go();
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
    UpdateClick(Id) {
      axios
        .put(`http://localhost:8080/api/task/updatetask/${this.userid}/` + Id, {
          taskName: this.taskname,
          status: this.status,
        })
        .then(() => {
          this.$router.go();
        });
    },
    AddClick() {
      axios
        .post(`http://localhost:8080/api/task/createtask/${this.userid}`, {
          taskName: this.taskname,
          status: this.status,
        })
        .then(() => {
          this.$router.go();
        });
    },
  },
};
</script>

<style scoped>
  body{
    width: 100%;
    height: 100%;
  }
</style>
